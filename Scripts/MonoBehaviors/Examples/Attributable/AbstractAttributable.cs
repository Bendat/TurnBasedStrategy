using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Examples.Attributable
{
    public abstract class AbstractAttributable<T>: MonoBehaviour where T : struct, IUnitAttribute
    {

        public T AttributesField1 => AttributesField;


        [SerializeField] protected T AttributesField;

        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {
		
        }
    }
}
