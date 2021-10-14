using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jniac {

    public class DontTouchMe : MonoBehaviour {

        public GameObject playerPrefab;
        public GameObject[] particles = new GameObject[0];
        public int particleCount = 30;

        TMPro.TextMeshPro text;

        void Start() {
            text = GetComponentInChildren<TMPro.TextMeshPro>();
        }

        void OnTriggerEnter(Collider other) {

            bool isPlayer = other.GetComponent<Player>() != null;

            if (isPlayer) {
                Destroy(other.gameObject);
                StartCoroutine(IToldYou());
            }
        }

        void Boom() {
            if (particles.Length > 0) {
                for (int i = 0; i < particleCount; i++) {
                    GameObject source = particles[Random.Range(0, particles.Length)];
                    GameObject clone = Instantiate(source);
                    clone.transform.localScale = Vector3.one * Random.Range(0.1f, 0.5f);

                    Vector3 direction = Random.onUnitSphere;
                    clone.transform.position = transform.position + direction * 0.5f;
                    clone.GetComponent<Rigidbody>().velocity = direction * Random.Range(10f, 20f);
                }
            }
        }

        IEnumerator IToldYou() {

            Boom();

            yield return new WaitForSeconds(0.1f);

            text.text = "I told you!";

            yield return new WaitForSeconds(1f);

            int numberOfSeconds = 8;
            for (int i = 0; i < numberOfSeconds; i++) {
                text.text = $"{numberOfSeconds - i}...";
                yield return new WaitForSeconds(1f);
            }
            
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.transform.position = transform.position + Vector3.up * 20f + Vector3.right * 3f + Vector3.forward * 4f;

            text.text = "Don't touch me ok?";

            yield return new WaitForSeconds(1f);

            newPlayer.GetComponent<Player>().CameraTrack();
        }
    }
}
