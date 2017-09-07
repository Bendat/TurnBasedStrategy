using System;
using System.Collections.Generic;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Examples.Attributable
{
    /*
     * Interface to be shared by attribute structs which inherit it.
     * We want a way to store attributes without caring what specifc set of attributes they are.
     */
    public interface IAttribute
    {
        /*
         * This will return all the values in the attribute struct as an Enumerable dictionary of KeyValuePair<string, int>,
         * where string is the name of the attribute and int is its value. 
         * The value doesnt have to be an int, but for mixed-type attributes you'll need to come up with your own solution to this.
         * You can store them as <string, object> pairs, and use pattern matching. 
         * ie.
         * if(kvp.Value is int x){processAsInt(x)
         * else if(kvp.Value is string x){processAsString(x)
         */
        Dictionary<String, int> ToDictionary();
    }
}