using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Platformer.Mechanics
{
    public class TextBoxManager : MonoBehaviour
    {

        public GameObject textBox;
        public TextMeshProUGUI theText;

        public TextAsset textfile;
        public string[] textlines;

        public int currentline;
        public int endAtLine;

        public PlayerController player;

        public bool stopPlayer;

        // Start is called before the first frame update
        void Start()
        {


            if (textfile != null)
            {
                textlines = (textfile.text.Split('\n'));
            }


            if (endAtLine == 0)
            {
                endAtLine = textlines.Length - 1;

            }
        }

        void Update()
        {
            theText.text = textlines[currentline];

            if (Input.GetKeyDown(KeyCode.Return))
            {
                currentline += 1;
            }

            if (currentline > endAtLine)
            {
                textBox.SetActive(false);
                player.canMove = true;
            }

            if (stopPlayer)
            {
                player.canMove = false;
            }
        }
    }

}
