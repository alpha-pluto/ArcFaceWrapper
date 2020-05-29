/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-12  
 * daniel.zhang
 * zamen@126.com
 * 文件名：IFaceDataRepository.cs  
 * 文件功能描述：人脸数据仓库接口
 * 
----------------------------------------------------------------*/
using Afw.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afw.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IFaceDataRepository<TEntity> where TEntity : BaseEntity
    {
        #region path

        /// <summary>
        /// 人脸数据文件的目录
        /// </summary>
        /// <returns></returns>
        string GetFilePath();

        /// <summary>
        /// 人脸数据文件的全名（包括完整路径）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        string GetFileFullPath(int Id);

        /// <summary>
        /// 人脸图片的存放路径
        /// </summary>
        /// <returns></returns>
        string GetFaceImagePath();

        /// <summary>
        /// 人脸图片的完整路径
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        string GetFaceImageFullPath(int Id, string sourcePath);

        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        TEntity GetById(object id);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<TEntity> entities);

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        #endregion
    }
}

