using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;

namespace YaoHuo.Plugin.WebSite
{
    public class WapVCountEveryDateBLL : wap_vcount_everyDate_BLL
    {
        private string aSelf = null;

        public WapVCountEveryDateBLL(string InstanceName) : base(InstanceName)
        {
            aSelf = InstanceName;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="siteid"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public new wap_vcount_everyDate_Model GetModel_Today(long siteid, long types)
        {
            return new WapVCountEveryDateDAL(aSelf).GetModel_Today(siteid, types);
        }
    }
}
