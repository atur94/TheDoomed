using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
//    private EnemySelectorSystem _enemySelectorSystem;
//    
//    private bool _spawnModeOn = false;
//    private bool _canBuild = false;
//
//    [SerializeField] private LayerMask spawnPlaneLayerMask;
//
//    private GameObject _currentEnemyTemplate;
//    private Vector3 _spawnPoint;
//
//    void Update()
//    {
//        if (Input.GetKeyDown("e"))
//        {
//            _spawnModeOn = !_spawnModeOn;
//        }
//
//        if (_spawnModeOn)
//        {
//            RaycastHit spawnPostHit;
//
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//            if (Physics.Raycast(ray, out spawnPostHit, Mathf.Infinity, spawnPlaneLayerMask))
//            {
//                Vector3 point = spawnPostHit.point;
//                _spawnPoint = point;
//                _canBuild = true;
//            }
//            else
//            {
//                _canBuild = false;
//            }
//
//            if (_canBuild && _currentEnemyTemplate == null)
//            {
////                Enemy spawnEnemy = _enemySelectorSystem.allEnemies[0];
////                _currentEnemyTemplate = Instantiate(spawnEnemy.characterPrefab, _spawnPoint, Quaternion.identity);
////                _currentEnemyTemplate.layer = 2;
////                Enemy spawnedEnemy = _currentEnemyTemplate.AddComponent<Enemy>();
////                spawnedEnemy = spawnEnemy;
//            }
//
//            if (_canBuild && _currentEnemyTemplate != null)
//            {
//                _currentEnemyTemplate.transform.predictedPosition = _spawnPoint;
//
//                if (Input.GetMouseButtonDown(0))
//                {
//                    Instantiate(_currentEnemyTemplate).layer = 9;
//                }
//            }
//
//            if (!_canBuild && _currentEnemyTemplate != null)
//            {
//                Destroy(_currentEnemyTemplate);
//            }
//
//            if (!_spawnModeOn && _currentEnemyTemplate != null)
//            {
//                Destroy(_currentEnemyTemplate);
//                _canBuild = false;
//            }
//        }
//    }
//
//    void Start()
//    {
//        _enemySelectorSystem = GetComponent<EnemySelectorSystem>();
//    }
}
