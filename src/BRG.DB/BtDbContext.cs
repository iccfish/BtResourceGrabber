namespace BRG.DB
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Data;
	using System.Data.SQLite;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using BRG;
	using BRG.Entities;
	using BRG.Service;
	using Dapper;

	[Export(typeof(IDataContext))]
	class BtDbContext : IDataContext
	{
		#region 初始化和版本升级

		static string _connectString;

		public void Init()
		{
			var dbpath = PathUtility.Combine(AppContext.Instance.ConfigLoader.Root, "storage");
			if (!File.Exists(dbpath))
			{
				System.Data.SQLite.SQLiteConnection.CreateFile(dbpath);
			}

			var scsb = new SQLiteConnectionStringBuilder();
			scsb.DataSource = dbpath;
			_connectString = scsb.ToString();

			var connection = new SQLiteConnection(_connectString);
			connection.Open();

			//check inited;
			var tableExists = (long)connection.CreateCommand("select COUNT(*) from sqlite_master where type='table' and tbl_name='Version'", false).ExecuteScalar() > 0;
			var version = !tableExists ? new Version("0.0.0.0") : new Version((connection.CreateCommand("select [Version] from version where key='sys'", false).ExecuteScalar() ?? "0.0.0.0").ToString());

			//check version
			if (version.Major == 0)
			{
				var sqls = new[]
							{
								@"
								CREATE TABLE [PreviewInfo] (
									[Stype] INT NOT NULL,
									[Hash] NVARCHAR(128) NOT NULL,
									[PreviewSource] NVARCHAR(50),
									[PreviewType] INT NOT NULL,
									[WebUrl] VARCHAR(500),
									[ImageUrl] VARCHAR(500),
									[Description] NVARCHAR(1024),
									[LastUsed] DATETIME NOT NULL DEFAULT (datetime('now')),
									[HitCount] INT NOT NULL DEFAULT 1,
									CONSTRAINT [] PRIMARY KEY ([Stype], [Hash], [PreviewSource]) ON CONFLICT REPLACE);",
								@"
								CREATE TABLE [Illegal] (
									[SType] INT NOT NULL,
									[Hash] VARCHAR(128) NOT NULL,
									[State] INT NOT NULL,
									[ReportCount] INT NOT NULL,
									[UpdateTime] DATETIME NOT NULL,
									CONSTRAINT [] PRIMARY KEY ([SType], [Hash]) ON CONFLICT REPLACE);",
								@"
								CREATE TABLE [Version] (
									[Key] NVARCHAR(50) NOT NULL, [Version] NVARCHAR(50), CONSTRAINT [] PRIMARY KEY ([Key]) ON CONFLICT REPLACE);",
								@"
								INSERT INTO Version ([Key], [Version]) VALUES ('sys','1.0.0.0')"
							};

				Array.ForEach(sqls, s => connection.CreateCommand(s, false).ExecuteNonQuery());
				version = new Version("1.0.0.0");
			}

			connection.Close();
		}

		SQLiteConnection _connection;

		public BtDbContext()
		{
			_connection = new SQLiteConnection(_connectString);
		}

		#endregion


		#region 预览信息

		/// <summary>
		/// 根据Hash加载预览信息
		/// </summary>
		/// <param name="hashes"></param>
		/// <returns></returns>
		public PreviewInfoStore[] PreviewInfo_Load(ResourceType stype, params string[] hashes)
		{
			if (hashes == null || hashes.Length == 0)
				return new PreviewInfoStore[0];

			var sql = string.Format("UPDATE PreviewInfo SET HitCount=HitCount+1, LastUsed=datetime('now') where stype={1} and hash in ({0}) and previewsource!=''; select pi.* from previewinfo pi where stype={1} and hash in ({0})", hashes.Select(s => "'" + s + "'").JoinAsString(","), (int)stype);
			return _connection.Query<PreviewInfoStore>(sql).ToArray();
		}

		/// <summary>
		/// 同步资源信息以及填充资源预览信息。
		/// </summary>
		/// <param name="infos"></param>
		public void PreviewInfo_Sync(IResourceSearchInfo infos)
		{
			var types = infos.GroupBy(s => s.ResourceType);

			//按照资源类型分组以便于查询
			List<IResourceInfo> updatablePreviewInfo = new List<IResourceInfo>();
			HashSet<IResourceInfo> noValidInfo = new HashSet<IResourceInfo>();
			foreach (var resourceType in types)
			{
				var hashes = resourceType.Where(s => s.IsHashLoaded).Select(s => s.Hash).ToArray();
				if (hashes.IsEmpty())
					continue;

				//加载资源预览信息
				var data = PreviewInfo_Load(resourceType.Key, hashes);
				foreach (var resinfo in resourceType)
				{
					//信息没加载完全，则跳过
					if (!resinfo.IsHashLoaded)
					{
						resinfo.DetailLoaded += (s, e) =>
						{
							PreviewInfo_Sync(new ResourceSearchInfo(infos.Provider) { s as IResourceInfo });
						};
						continue;
					}

					var info = data.FirstOrDefault(s => s.PreviewSource == resinfo.Provider.Info.Name && s.Hash == resinfo.Hash && s.SType == resinfo.ResourceType);
					var type = resinfo.SupportPreivewType;

					//如果没有预览信息，且现在有新的预览信息
					if (info == null && type != PreviewType.None && resinfo.PreviewInfo != null)
					{
						Trace.TraceInformation($"[DB] Insert preview info. Hash={resinfo.Hash}, Engine={resinfo.Provider.Info.Name}");
						//INSERT
						_connection.Execute("INSERT INTO PreviewInfo (SType, Hash,PreviewSource,PreviewType, WebUrl, ImageUrl, Description, LastUsed, HitCount) VALUES (@stype, @hash, @source, @previewType, @weburl, @imageurl, @desc, datetime('now'), 1)",
											new
											{
												stype = (int)resourceType.Key,
												hash = resinfo.Hash,
												source = resinfo.Provider.Info.Name,
												previewType = (int)type,
												weburl = resinfo.PreviewInfo.WebUrl,
												imageurl = resinfo.PreviewInfo.ImageUrl,
												desc = resinfo.PreviewInfo.Description
											});
						updatablePreviewInfo.Add(resinfo);
					}
					else if (info != null && resinfo.PreviewInfo != null && type != PreviewType.None && (resinfo.PreviewInfo.WebUrl != info.WebUrl || resinfo.SupportPreivewType != type || resinfo.PreviewInfo.ImageUrl != info.ImageUrl || resinfo.PreviewInfo.Description != info.Description))
					{
						Trace.TraceInformation($"[DB] Update preview info. Hash={resinfo.Hash}, Engine={resinfo.Provider.Info.Name}");
						//已有预览信息，且现在也有预览信息，且不一样，则更新
						_connection.Execute("UPDATE PreviewInfo SET PreviewType=@previewType, WebUrl=@weburl, ImageUrl=@imageurl, Description=@desc, HitCount=HitCount+1, LastUsed=datetime('now') WHERE SType=@stype AND Hash=@hash AND PreviewSource=@source",
											new
											{
												stype = (int)resourceType.Key,
												hash = resinfo.Hash,
												source = resinfo.Provider.Info.Name,
												previewType = (int)type,
												weburl = resinfo.PreviewInfo.WebUrl,
												imageurl = resinfo.PreviewInfo.ImageUrl,
												desc = resinfo.PreviewInfo.Description
											});
						updatablePreviewInfo.Add(resinfo);
					}
					else if (info != null && info.PreviewType != resinfo.SupportPreivewType && resinfo.SupportPreivewType == PreviewType.None)
					{
						Trace.TraceInformation($"[DB] Remove preview info. Hash={info.Hash}, Engine={resinfo.Provider.Info.Name}");
						//DELETE
						_connection.Execute("DELETE FROM PreviewInfo WHERE SType=@stype AND Hash=@hash AND Source=@source", new
						{
							stype = (int)resourceType.Key,
							hash = resinfo.Hash,
							source = resinfo.Provider.Info.Name
						});
					}
					else if (info != null && resinfo.SupportPreivewType != PreviewType.None && resinfo.PreviewInfo == null)
					{
						//set cached preview info.
						resinfo.PreviewInfo = info;
					}
					resinfo.PreviewInfo = resinfo.PreviewInfo ?? info;
					resinfo.SupportPreivewType = info?.PreviewType ?? resinfo.SupportPreivewType;

					//如果不支持预览或没有加载预览信息，则查找是否有相同的预览信息
					if (resinfo.PreviewInfo == null)
					{
						info = data.FirstOrDefault(s => s.Hash == resinfo.Hash && s.SType == resinfo.ResourceType && s.PreviewSource != "");
						resinfo.PreviewInfo = resinfo.PreviewInfo ?? info;
						resinfo.SupportPreivewType = info?.PreviewType ?? 0;
					}
					if (resinfo.PreviewInfo == null && data.Any(s => s.Hash == resinfo.Hash && s.SType == resinfo.ResourceType && s.PreviewSource == ""))
					{
						resinfo.SupportPreivewType = PreviewType.None;
						noValidInfo.Add(resinfo);
					}

					resinfo.PreviewInfoLoaded += (s, e) =>
					{
						PreviewInfo_Sync(new ResourceSearchInfo(infos.Provider) { s as IResourceInfo });
					};
				}
			}
		}

		public void PreviewInfo_MarkEmptyPreview(ResourceType stype, string hash)
		{
			_connection.Execute("INSERT INTO PreviewInfo (SType, Hash,PreviewSource,PreviewType, WebUrl, ImageUrl, Description) VALUES (@hash, @source, @previewType, null, null, null)",
								new
								{
									stype = (int)stype,
									hash,
									source = "",
									prevewType = (int)PreviewType.None
								});

		}

		public void PreviewInfo_Update(ResourceType stype, string engine, string hash, PreviewType type, PreviewInfo info)
		{
			var pinfo = _connection.Query<PreviewInfoStore>("SELECT * FROM PreviewInfo WHERE SType=@stype, Hash=@hash AND Source=@source", new
			{
				stype = (int)stype,
				hash,
				source = engine

			}).FirstOrDefault();

			if (info != null && type != PreviewType.None)
			{
				if (pinfo == null)
				{
					//INSERT
					_connection.Execute("INSERT INTO PreviewInfo (SType, Hash,PreviewSource,PreviewType, WebUrl, ImageUrl, Description) VALUES (@hash, @source, @previewType, @weburl, @imageurl, @desc)",
						new
						{
							stype = (int)stype,
							hash,
							source = engine,
							prevewType = (int)type,
							weburl = info.WebUrl,
							imageurl = info.ImageUrl,
							description = info.Description
						});
				}
				else if (pinfo.WebUrl != info.WebUrl || pinfo.PreviewType != type || pinfo.ImageUrl != info.ImageUrl || pinfo.Description != info.Description)
				{
					_connection.Execute("UPDATE PreviewInfo SET PreviewType=@type, WebUrl=@weburl, ImageUrl=@imageurl, Description=@description WHERE SType=@stype AND Hash=@hash AND PreviewSource=@source",
						new
						{
							stype = (int)stype,
							hash,
							source = engine,
							prevewType = (int)type,
							weburl = info.WebUrl,
							imageurl = info.ImageUrl,
							description = info.Description
						});
				}
			}
			else
			{
				if (pinfo != null)
				{
					//DELETE
					_connection.Execute("DELETE FROM PreviewInfo WHERE SType=@stype AND Hash=@hash AND Source=@source", new
					{
						stype = (int)stype,
						hash,
						source = engine

					});
				}
			}
		}


		#endregion

		#region 资源安全性确认

		public void VerifyState_Sync(IResourceSearchInfo infos)
		{
			//先自动判定
			AppContext.Instance.SecurityCheck.Check(infos);
		}

		public void VerifyState_Update(IResourceInfo info)
		{
			if (info.VerifyState == VerifyState.Unknown || info.VerifyState == VerifyState.AutoIllegal || info.VerifyState == VerifyState.AutoFake)
				return;

			var query = @"UPDATE Illegal SET ReportCount=@reportCount, UpdateTime=datetime('now'), state=@state WHERE SType=@type AND Hash=@hash";
			var paramObj = new
			{
				hash = info.Hash,
				type = (int)info.ResourceType,
				reportCount = info.ReportNum,
				state = (int)info.VerifyState
			};
			if (_connection.Execute(query, paramObj) == 0)
			{
				query = @"INSERT INTO Illegal (SType, Hash, State, ReportCount, UpdateTime) VALUES (@type, @hash, @state, @reportCount, datetime('now'))";
				_connection.Execute(query, paramObj);
			}
		}

		#endregion

		#region 已下载记录

		public void DownloadHistory_Sync(IResourceSearchInfo infos)
		{
			var types = infos.Where(s => s.ResourceType == ResourceType.Ed2K || s.ResourceType == ResourceType.BitTorrent).GroupBy(s => s.ResourceType);
			var dh = AppContext.Instance.DownloadHistory;

			foreach (var type in types)
			{
				foreach (var info in type)
				{
					if (info.IsHashLoaded)
					{
						info.ChangeDownloadedStatus(dh.ContainsKey(info.Hash));
						info.DownloadedChanged += (s, e) => DownloadHistory_Update(s as IResourceInfo);
					}
					else
					{
						info.DetailLoaded += (s, e) =>
						{
							DownloadHistory_Sync(new ResourceSearchInfo(infos.Provider) { s as IResourceInfo });
						};
					}
				}
			}
		}

		public void DownloadHistory_Update(IResourceInfo info)
		{
			if (!info.IsHashLoaded)
				return;

			var dh = AppContext.Instance.DownloadHistory;
			if (info.Downloaded)
			{
				if (dh.ContainsKey(info.Hash))
					dh.Remove(info.Hash);
			}
			else if (!dh.ContainsKey(info.Hash))
			{
				dh.Add(info.Hash, new HistoryItem() { Title = info.Title, DownloadTime = DateTime.Now });
			}

		}

		#endregion


		#region Dispose方法实现

		bool _disposed;

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			_disposed = true;

			if (disposing)
			{
				_connection.Close();

			}
			//TODO 释放非托管资源

			//挂起终结器
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 检查是否已经被销毁。如果被销毁，则抛出异常
		/// </summary>
		/// <exception cref="ObjectDisposedException">对象已被销毁</exception>
		protected void CheckDisposed()
		{
			if (_disposed)
				throw new ObjectDisposedException(this.GetType().Name);
		}


		#endregion
	}
}
