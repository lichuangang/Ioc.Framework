using Li.Framework.Core.Ioc;
using Li.Framework.Dtos;
using Li.Framework.Entitys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Repositorys
{
    /* ==============================================================================
     * 描述：BaseRepository
     * 创建人：李传刚 2017/7/14 14:35:38
     * ==============================================================================
     */
    public class BaseRepository<T, Key> where T : IdKey<Key>, new()
    {
        SqlSugarClient _db;
        ISugarQueryable<T> _table;

        public BaseRepository()
            : this(ContainerManager.Resolve<SqlSugarClient>())
        {

        }

        public BaseRepository(SqlSugarClient db)
        {
            _db = db;
            _table = db.Queryable<T>();
        }

        #region 查询

        public PageResult<T> GetPage(QueryPage page, Expression<Func<T, bool>> where = null)
        {
            int total = 0;
            ISugarQueryable<T> data = _db.Queryable<T>();

            if (where != null)
            {
                data = data.Where(where);
            }

            if (string.IsNullOrWhiteSpace(page.SortField))
            {
                data.OrderBy(page.SortField + " " + page.Order);
            }

            return new PageResult<T>()
            {
                Data = data.ToPageList(page.PageIndex, page.PageSize, ref total),
                Total = total
            };

        }

        public virtual T GetById(Key id)
        {
            return _table.InSingle(id);
        }

        public List<T> Find(Expression<Func<T, bool>> where)
        {
            return _table.Where(where).ToList();
        }
        #endregion

        #region 增加
        public int Insert(T t, bool isIdentity = false)
        {
            if (isIdentity)
            {
                return _db.Insertable(t).ExecuteReutrnIdentity();
            }

            return _db.Insertable(t).ExecuteCommand();
        }
        #endregion

        #region 修改
        /// <summary>
        /// 根据ID,更新实体所有字段值
        /// </summary>
        public int Update(T t)
        {
            return _db.Updateable(t).ExecuteCommand();
        }
        /// <summary>
        /// 根据ID 更新某些列
        /// </summary>
        public int Update(T key, Expression<Func<T, object>> columns)
        {
            return _db.Updateable(key).UpdateColumns(columns).ExecuteCommand();
        }
        /// <summary>
        /// 根据where条件,更新某些列
        /// </summary>
        public int Update(Expression<Func<T, T>> columns, Expression<Func<T, bool>> where)
        {
            return _db.Updateable<T>().UpdateColumns(columns).Where(where).ExecuteCommand();
        }
        #endregion

        #region 删除
        public int Delete(T t)
        {
            return _db.Deleteable(t).ExecuteCommand();
        }

        public int DeleteById(Key id)
        {
            return _db.Deleteable<T>().In(id).ExecuteCommand();
        }
        #endregion
    }
}
