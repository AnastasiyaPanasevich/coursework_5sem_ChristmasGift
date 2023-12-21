using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftLib;

namespace GiftServer.Model
{
    public static class GiftCreator
    {
        public static List<Gift> CreateRandomGift(int n, double budget)
        {
            //здесь должна быть логика для загрузки из БД

            return null;
        }

        public static Gift CreateCustomGift(int[] indexes)
        {
            return null;
        }

        static List<int> FindCombination(List<int> prices, List<int> quantities, int n, int budget)
        {
            int[] dp = new int[budget + 1];

            for (int i = 0; i < n; i++)
            {
                for (int j = budget; j >= prices[i]; j--)
                {
                    dp[j] = Math.Max(dp[j], dp[j - prices[i]] + quantities[i]);
                }
            }

            List<int> indexes = new List<int>();
            int remainingBudget = budget;
            for (int i = n - 1; i >= 0 && remainingBudget > 0; i--)
            {
                if (i == 0 && dp[remainingBudget] == quantities[i])
                {
                    indexes.Add(i);
                    break;
                }

                if (i > 0 && dp[remainingBudget] != dp[remainingBudget - prices[i]] + quantities[i])
                {
                    indexes.Add(i);
                    remainingBudget -= prices[i];
                }
            }

            return indexes;
        }
    }
}
