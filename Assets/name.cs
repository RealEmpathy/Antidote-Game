using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    public class name : MonoBehaviour
    {
        public Button button;
        public Text text;
        // Start is called before the first frame update
        void Awake()
        {
            button = GetComponent<Button>();
            foreach (Transform t in transform)
            { // might not be obvious
                text = t.GetComponent<Text>();
            }
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}