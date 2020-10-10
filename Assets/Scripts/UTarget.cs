using System;
using System.Collections;
using UnityEngine;

namespace MyFirstGame.Assets.Scripts
{
    public class UTarget : MonoBehaviour
    {

        public bool frozen;
        public float speed;
        public float spin;

        protected void Start()
        {
            StartCoroutine(_Bounce());
            StartCoroutine(_Spin());
        }

        public IEnumerator RunAway(Func<Vector3> getPosition)
        {
            while (true)
            {
                var positionOther = getPosition();
                var positionSelf = transform.position;
                var direction = (positionSelf - positionOther).normalized;
                direction.y = 0;
                positionSelf += direction * speed * Time.deltaTime;
                transform.position = positionSelf;

                yield return null;
            }
        }

        private IEnumerator _Spin()
        {
            while (true)
            {
                transform.Rotate(new Vector3(spin * speed * Time.deltaTime, spin * speed * Time.deltaTime, spin * speed *  Time.deltaTime));
                yield return null;
            }
        }

        private IEnumerator _Bounce()
        {
            var y = transform.position.y;
            var value = 0f;

            while (true)
            {
                var position = transform.position;
                value += Time.deltaTime;
                position.y = y + Mathf.Sin(value);
                transform.position = position;

                if (frozen)
                {
                    yield return new WaitUntil(() => !frozen);
                }
                else
                {
                    yield return null;
                }

            }

            //Debug.Log("Here!");
            //yield return null;
            //Debug.Log("And here!");
        }

        //public int bounce;
        //private bool moveUp = false;
        //private int maxHeight = 3;
        //private int minHeight = 1;

        //private float _y;
        //private float _value;

        //public void Update()
        //{
        //    _y = transform.position.y;
        //    _Animate();
        //}

        //private void _Animate()
        //{
        //    //var position = transform.position;
        //    //_value += Time.deltaTime;
        //    //position.y = _y + Mathf.Sin(_value);
        //    //transform.position = position;

        //    Vector3 position = transform.position;
        //    var timeBounce = Time.deltaTime * bounce;
        //    if (moveUp)
        //    {
        //        position.y += timeBounce;
        //    }
        //    else
        //    {
        //        position.y -= timeBounce;
        //    }
        //    Debug.Log(new { position, timeBounce, moveUp });
        //    transform.position = position;
        //    if (transform.position.y > maxHeight)
        //    {
        //        moveUp = false;
        //    }
        //    else if (transform.position.y < minHeight)
        //    {
        //        moveUp = true;
        //    }
        //}
    }
}
