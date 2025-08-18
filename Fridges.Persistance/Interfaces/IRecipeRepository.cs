using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Domain.Enities.Recipe;

namespace Fridges.Persistance.Interfaces;

public interface IRecipeRepository
{
    Task<List<Recipe>> SearchRecipesByProductTypeId(int productTypeId);
}
