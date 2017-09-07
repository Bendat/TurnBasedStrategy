using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Examples.Attributable
{
    /**
     * Abstract class represnting any object that intends to use attributes.
     * It takes a generic parameter called T. T must be a value type (i.e, a struct) and it must implement IAttribute.
     * We could store attributes as public IAttribute or public IAttributes[], but then we'd be regularly boxing (casting to an object, because IAttribute is a reference type)
     * all our structs, which defeats the point of structs. By doing this we can store it as its proper type, ensuring that that
     * type inherits IAttribute
     */
    public abstract class AbstractAttributable<T>: MonoBehaviour where T : struct, IAttribute
    {
        /*
        * We want to provide access to our attributes, but we don't want them edited at run time because they
        * are structs, and mutating structs haphazardly is bad
        * */
        public T Attributes => AttributesField;

        /**
         * Field backing the Attributes property. It's protected and serialized. We can edit it from the editor, and from child classes.
         */
        [SerializeField] protected T AttributesField;

        public abstract Dictionary<string, int> GetAttributes();
    }
}
