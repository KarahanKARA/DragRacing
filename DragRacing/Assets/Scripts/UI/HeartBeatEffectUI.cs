using System.Collections;
using UnityEngine;

namespace UI
{
    public class HeartBeatEffectUI : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private float _onStartScaleX;
        private float _onStartScaleY;
        private float _scaleX, _scaleY;

        private void OnEnable()
        {
            _rectTransform = GetComponent<RectTransform>();
            var localScale = _rectTransform.localScale;
            _onStartScaleX = localScale.x;
            _onStartScaleY = localScale.y;
            _scaleX = _onStartScaleX;
            _scaleY = _onStartScaleY;
            StartCoroutine(Enlarge());
        }

        private IEnumerator Shrink()
        {
            yield return new WaitForSeconds(0.1f);
            while (true)
            {
                yield return new WaitForSeconds(0.05f);
                _scaleX -= 0.02f;
                _scaleY -= 0.02f;
                _rectTransform.localScale = new Vector2(_scaleX, _scaleY);
                if (_scaleX <= _onStartScaleX)
                {
                    StartCoroutine(Enlarge());
                    break;
                }
            }
        }

        private IEnumerator Enlarge()
        {
            yield return new WaitForSeconds(0.1f);
            while (true)
            {
                yield return new WaitForSeconds(0.05f);
                _scaleX += 0.02f;
                _scaleY += 0.02f;

                _rectTransform.localScale = new Vector2(_scaleX, _scaleY);
                if (_scaleX >= _onStartScaleX * 1.2f)
                {
                    StartCoroutine(Shrink());
                    break;
                }
            }
        }

        public void DestroyTutorialText()
        {
            Destroy(gameObject);
        }
    }
}
