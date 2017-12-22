using System.Collections;
using UnityEngine;

public class Corout {
    public Coroutine coroutine { get; private set; }
    public object result;
    private IEnumerator target;

    public Corout(MonoBehaviour owner, IEnumerator target) {
        this.target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run() {
        while(target.MoveNext()) {
            result = target.Current;
            yield return result;
        }
    }
}
