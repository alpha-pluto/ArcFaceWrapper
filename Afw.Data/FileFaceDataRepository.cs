/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-12  
 * daniel.zhang
 * zamen@126.com
 * 文件名：FileFaceDataRepository.cs  
 * 文件功能描述：人脸数据仓库操作实现类
 * 
----------------------------------------------------------------*/
using Afw.Data.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Afw.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class FileFaceDataRepository<T> : IFaceDataRepository<T> where T : BaseEntity
    {
        #region field

        private readonly string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        private readonly string subDir = System.Text.RegularExpressions.Regex.Replace(typeof(T).Name.ToString().ToLower(), @"\W", "", System.Text.RegularExpressions.RegexOptions.Compiled);

        private readonly string faceSubDir = "face";

        private static object ioLock = new object();

        #endregion

        #region property

        protected IDictionary<int, T> dataSet;

        public IDictionary<int, T> DataSet => dataSet;

        public virtual IQueryable<T> Table => dataSet.Select(x => x.Value).OrderBy(i => i.Id).AsQueryable();

        public virtual IQueryable<T> TableNoTracking => dataSet.Select(x => x.Value).OrderBy(i => i.Id).AsQueryable();


        #endregion

        #region ctor

        public FileFaceDataRepository()
        {
            dataSet = new Dictionary<int, T>(3000);

            LoadFaceData();
        }

        #endregion

        #region initial

        public virtual int LoadFaceData()
        {
            int ret = 0;
            lock (ioLock)
            {
                DirectoryInfo faceFolder = new DirectoryInfo(GetFilePath());
                if (faceFolder.Exists)
                {
                    foreach (FileInfo faceDataFile in faceFolder?.GetFiles())
                    {
                        //限定人脸数据文件小于1M,并且是数字命名以dat为后缀的文件 
                        if (faceDataFile.Length <= 1024 * 1024
                            && System.Text.RegularExpressions.Regex.IsMatch(faceDataFile.Name, @"(?i)^\d+\.dat$")
                            && faceDataFile.Extension.Equals(".dat", StringComparison.InvariantCultureIgnoreCase))
                        {
                            try
                            {
                                var entity = Data.Helper.BinaryEntityHelper.BinaryFileToEntity<T>(faceDataFile.FullName);
                                if (dataSet.ContainsKey(entity.Id))
                                {
                                    dataSet[entity.Id] = entity;
                                }
                                else
                                {
                                    dataSet.Add(entity.Id, entity);
                                }
                                ret++;
                            }
                            catch (Exception ex)
                            {
                                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Data.FileFaceDataRepository<T>), $"载入人脸数据文件出错：{faceDataFile.FullName} , 详情：{ex.ToString()}");
                            }
                        }
                    }
                }

            }

            return ret;
        }

        #endregion

        #region utility

        /// <summary>
        /// 人脸数据文件的目录
        /// </summary>
        /// <returns></returns>
        public string GetFilePath()
        {
            var path = $"{baseDir}{subDir}";
            return path;
        }

        /// <summary>
        /// 人脸数据文件的全名（包括完整路径）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetFileFullPath(int Id)
        {
            var path = $"{GetFilePath()}\\{Id}.dat";
            return path;
        }

        /// <summary>
        /// 人脸图片的存放路径
        /// </summary>
        /// <returns></returns>
        public string GetFaceImagePath()
        {
            var path = $"{GetFilePath()}\\{faceSubDir}";
            return path;
        }

        /// <summary>
        /// 人脸图片的完整路径
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        public string GetFaceImageFullPath(int Id, string sourcePath)
        {
            var path = $"{GetFilePath()}\\{faceSubDir}\\{Id}{Path.GetExtension(sourcePath)}";
            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        protected void DeleteFile(object id)
        {
            if (id is int)
            {
                var oid = Convert.ToInt32(id);
                var filePath = GetFileFullPath(oid);
                var entity = Afw.Data.Helper.BinaryEntityHelper.BinaryFileToEntity<T>(filePath);
                var t = typeof(T);
                var faceImagePath = (string)t.GetField("FaceImagePath")?.GetValue(entity);
                var entityOriginal = dataSet.ContainsKey(oid) ? dataSet[oid] : default(T);
                var faceImagePathOriginal = (string)t.GetField("FaceImagePath")?.GetValue(entityOriginal);
                if (!string.IsNullOrEmpty(Path.GetFileName(faceImagePath ?? "")))
                {
                    if (!faceImagePath.Equals(faceImagePathOriginal))
                    {
                        Task.Factory.StartNew(() =>
                        {
                            File.Delete(faceImagePath);
                        });
                    }

                }
                File.Delete(filePath);
            }
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            lock (ioLock)
            {
                try
                {
                    #region obsoleted

                    //var t = typeof(T);
                    //var id = (int?)t.GetField("Id")?.GetValue(entity);
                    //if (id.HasValue)
                    //{
                    //    var faceImagePath = (string)t.GetField("FaceImagePath")?.GetValue(entity);
                    //    var filePath = GetFileFullPath(id.Value);
                    //    if (!string.IsNullOrEmpty(Path.GetFileName(faceImagePath ?? "")))
                    //    {
                    //        Task.Factory.StartNew(() =>
                    //        {
                    //            File.Delete(faceImagePath);
                    //            File.Delete(filePath);
                    //        });

                    //    }
                    //    dataSet.Remove(id.Value);
                    //}

                    #endregion

                    var t = typeof(T);
                    var id = entity.Id;
                    if (id > 0)
                    {
                        var faceImagePath = (string)t.GetField("FaceImagePath")?.GetValue(entity);
                        var filePath = GetFileFullPath(id);
                        if (!string.IsNullOrEmpty(Path.GetFileName(faceImagePath ?? "")))
                        {
                            Task.Factory.StartNew(() =>
                            {
                                File.Delete(GetFaceImageFullPath(id, faceImagePath));
                                File.Delete(filePath);
                            });

                        }
                        dataSet.Remove(id);
                    }


                }
                catch (Exception ex)
                {
                    Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(this.GetType().ToString(), $"Exception When Delete => {ex.ToString()}");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(object id)
        {
            if (id is int)
            {
                var oid = Convert.ToInt32(id);
                var entity = dataSet.ContainsKey(oid) ? dataSet[oid] : default(T);
                if (entity.Equals(default(T)))
                {
                    var filePath = GetFileFullPath(oid);
                    entity = Afw.Data.Helper.BinaryEntityHelper.BinaryFileToEntity<T>(filePath);
                    if (entity != null)
                    {
                        dataSet[oid] = entity;
                    }
                }
                return entity;
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(T entity)
        {
            lock (ioLock)
            {
                try
                {
                    var t = typeof(T);
                    var faceImagePathSource = (string)t.GetField("FaceImagePath")?.GetValue(entity);
                    var filePath = GetFileFullPath(entity.Id);
                    if (!string.IsNullOrEmpty(Path.GetFileName(faceImagePathSource ?? "")))
                    {

                        var dataFilePath = GetFilePath();
                        if (!Directory.Exists(dataFilePath))
                            Directory.CreateDirectory(dataFilePath);

                        var faceFilePath = GetFaceImagePath();
                        if (!Directory.Exists(faceFilePath))
                            Directory.CreateDirectory(faceFilePath);
                        var faceImagePath = GetFaceImageFullPath(entity.Id, faceImagePathSource);
                        if (!faceImagePathSource.Equals(faceImagePath, StringComparison.InvariantCultureIgnoreCase))
                            File.Copy(faceImagePathSource, faceImagePath, true);
                        t.GetField("FaceImagePath")?.SetValue(entity, entity.Id.ToString() + Path.GetExtension(faceImagePathSource));
                    }
                    Afw.Data.Helper.BinaryEntityHelper.WriteEntityIntoFile<T>(entity, filePath);

                    if (!dataSet.ContainsKey(entity.Id))
                    {
                        dataSet.Add(entity.Id, entity);
                    }
                    else
                    {
                        dataSet[entity.Id] = entity;
                    }
                }
                catch (Exception ex)
                {
                    Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(this.GetType().ToString(), $"Exception When Insert => {ex.ToString()}");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void Insert(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            if (!dataSet.ContainsKey(entity.Id))
            {
                Insert(entity);
            }
            else
            {
                DeleteFile(entity.Id);
                Insert(entity);
            }
        }

        public void Update(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
