using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyFirstGame.Assets.Scripts
{
    public class UPlayer : MonoBehaviour
    {
        public float speed;
        public float spin;
        public List<UTarget> targets;

        private Dictionary<UTarget, Coroutine> _targetsFleeing = new Dictionary<UTarget, Coroutine>();

        // Start is called before the first frame update
        protected void Start()
        {

        }

        // Update is called once per frame
        protected void Update()
        {
            _MoveSelf();
            _DestroyTarget();
        }

        private void _MoveSelf()
        {
            var up = Input.GetKey(KeyCode.UpArrow);
            if (up)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
                // Vector3 position = transform.position;
                // position.z -= speed * Time.deltaTime;
                // transform.position = position;
            }
            var down = Input.GetKey(KeyCode.DownArrow);
            if (down)
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
                //Vector3 position = transform.position;
                //position.z += speed * Time.deltaTime;
                //transform.position = position;
            }
            var left = Input.GetKey(KeyCode.LeftArrow);
            if (left)
            {
                transform.Rotate(new Vector3(0, -spin * Time.deltaTime, 0));
                //Vector3 position = transform.position;
                //position.x += speed * Time.deltaTime;
                //transform.position = position;
            }
            var right = Input.GetKey(KeyCode.RightArrow);
            if (right)
            {
                transform.Rotate(new Vector3(0, spin * Time.deltaTime, 0));
                //Vector3 position = transform.position;
                //position.x -= speed * Time.deltaTime;
                //transform.position = position;
            }
        }

        private void _DestroyTarget()
        {
            var newTargets = new List<UTarget>();
            targets.ForEach((target) =>
            {
                Debug.Log("target");
                var distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance <= 1)
                {
                    Debug.Log("target destroyed");
                    //target.frozen = !target.frozen;
                    Destroy(target.gameObject);
                    return;
                }
                else if (distance < 10 && distance < 20)
                {
                    Debug.Log("target fleeing");
                    if (!_targetsFleeing.ContainsKey(target))
                    {
                        var runAwayCoroutine = StartCoroutine(target.RunAway(() => transform.position));
                        _targetsFleeing.Add(target, runAwayCoroutine);
                    }
                }
                else if (distance > 20)
                {
                    Debug.Log("target stop fleeing");
                    if (_targetsFleeing.TryGetValue(target, out var runAwayCoroutine))
                    {
                        StopCoroutine(runAwayCoroutine);
                        _targetsFleeing.Remove(target);
                    }
                }
                Debug.Log("target survived");
                newTargets.Add(target);
            });
            targets = newTargets;
        }
    }
}