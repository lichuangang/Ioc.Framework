using Li.Framework.Entitys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Repository
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

        public BaseRepository(SqlSugarClient db)
        {
            _db = db;
            _table = db.Queryable<T>();
        }

        #region 查询
        public T GetById(Key id)
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

        public int Insert(T t)
        {
            return _db.Insertable(t).ExecuteCommand();
        }

        #endregion

        #region 修改
        public int Update(T t)
        {
            return _db.Updateable(t).ExecuteCommand();
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
