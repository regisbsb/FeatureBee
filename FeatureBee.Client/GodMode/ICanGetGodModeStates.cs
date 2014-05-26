using FeatureBee.WireUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FeatureBee.GodMode
{
    public interface ICanGetGodModeStates
    {
        GodModeFeatureCollection GetGodModeFeatures(HttpRequestBase request);
    }
}
