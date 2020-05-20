using Mt.Edu.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mt.Edu.Api.Factories
{
    public static class ClaMapper
    {
        public static IEnumerable<Tree> ToTrees(this IEnumerable<Cla> clas)
        {
            if (clas == null)
                return null;

            return clas.Select(a => a.ToTree());
        }

        public static Tree ToTree(this Cla cla)
        {
            if (cla == null)
                return null;

            return new Tree(cla.Id, cla.Name, cla.ParentId);
        }
    }
}
