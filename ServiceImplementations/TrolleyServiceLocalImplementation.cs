using eXercise.Entities;
using eXercise.ServiceInterfaces;
using System;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ServiceImplementations
{
    public class TrolleyServiceLocalImplementation : ITrolleyService
    {
        public async Task<decimal> GetTrolleyTotalAsync(TrolleyRequest trolleyRequest)
        {
            if (trolleyRequest != null)
            {
                await Task.CompletedTask;
                
                var jsonDebug = JsonConvert.SerializeObject(trolleyRequest);

                decimal total = 0;

                foreach (var item in trolleyRequest.quantities)
                {
                    var itemOriginalPrice = (from p in trolleyRequest.products
                                     where string.Compare(p.name, item.name, ignoreCase: true) == 0
                                     select p.price).FirstOrDefault<decimal>();

                    var matchingSpecial = FetchMatchingSpecial(trolleyRequest.specials, item.name, item.quantity);

                    if(matchingSpecial != null)
                    {
                        var discountCalculated = (itemOriginalPrice / 100) * matchingSpecial.total;
                        var priceAfterDiscount = (itemOriginalPrice - discountCalculated);
                        total += priceAfterDiscount * item.quantity;
                    }
                    else
                    {
                        total += itemOriginalPrice * item.quantity;
                    }

                }

                return Math.Round(total, MidpointRounding.ToZero);
            }
            else
            {
                throw new Exception("Invalid trolley request");
            }
        }


        private Specials FetchMatchingSpecial(IEnumerable<Specials> specialsData, string productName, int productQuantity)
        {
            //var orderedSpecials = specialsData.OrderByDescending(s => s.total);
            
            foreach(var special in specialsData)
            {
                var resultSpecials = (from q in special.quantities
                         where q.name == productName && q.quantity == productQuantity
                         select q).AsEnumerable<Special>();

                
                if(resultSpecials != null)
                {
                    return special;
                }
                else
                {
                    continue;
                }
            }

            return null;
        }
    }
}
