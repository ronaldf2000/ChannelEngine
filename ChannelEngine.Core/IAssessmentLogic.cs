using ChannelEngine.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelEngine.Core
{
    public interface IAssessmentLogic
    {
        Task<IEnumerable<TopResultView>> RetreiveTop(int number);
    }
}