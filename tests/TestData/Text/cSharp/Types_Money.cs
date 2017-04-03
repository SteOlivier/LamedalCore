using System;

namespace Blueprint.lib.Rules.Types
{
    /// <summary>
    /// Money convertions
    /// </summary>
    [BlueprintRule_Class(enBlueprintClassNetworkType.Node_Action)]
    [BlueprintCodeInjection_(typeof(Controller_BlueprintLogger), true)]
    public sealed class Types_Money
    {
        public string Method1()
        {
            return "Test";
        }
        
        /// <summary>
        ///     A Double extension method that converts the @this to a money.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a Double.</returns>
        public Double ToMoney(Double @this)
        {
            return Math.Round(@this, 2);
        }
    }
}