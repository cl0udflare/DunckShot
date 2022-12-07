using System.Collections.Generic;
using UnityEngine;

public class BasketCreator : MonoBehaviour
{
    [SerializeField] private Basket _basketPrefab;
    [SerializeField] private List<Basket> spawnedBasket;
    [SerializeField] private Pause _pause;

    private float _mainPositionX;
    private float MainPositionY => spawnedBasket[^1].transform.position.y;

    private void Awake() => _mainPositionX = spawnedBasket[0].transform.position.x;

    private void Update()
    {
        if (spawnedBasket[^1]._isSelected)
            SpawnBasket();
        
        spawnedBasket[0]._isSelected = _pause.isActiveAndEnabled;
    }

    private void SpawnBasket()
    {
        var multiply = spawnedBasket[^1].transform.position.x > 0 ? 1 : -1;

        var position = new Vector2(_mainPositionX * multiply + Random.Range(-0.4f, 0.5f),
            MainPositionY + 2 + Random.Range(-1, 1));

        var newBasket = Instantiate(_basketPrefab, position, Quaternion.identity);
        spawnedBasket.Add(newBasket);

        if (spawnedBasket.Count >= 3)
        {
            Destroy(spawnedBasket[0].gameObject);
            spawnedBasket.RemoveAt(0);
        }
    }
}