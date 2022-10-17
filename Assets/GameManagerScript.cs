using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GameManagerScript : MonoBehaviour
{
    public GameObject _player;
    public GameObject _tileMap;
    public List<WorldTile> _worldTiles;
    public WorldTile _currentTile;
    public GameObject _currentTileIndicator;
    public GameObject _PrefabTilledTile;
    public GameObject _PrefabPot;
    

    // Start is called before the first frame update
    void Start()
    {
        _worldTiles = GetWorldTiles();
    }

    public List<WorldTile> GetWorldTiles(){
        var tileMap = _tileMap.GetComponent<Tilemap>();
        var tileList = new List<WorldTile>();
  
		foreach (Vector3Int pos in tileMap.cellBounds.allPositionsWithin)
		{
			var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

			if (!tileMap.HasTile(localPlace)) continue;
			var tile = new WorldTile
			{
				LocalPlace = localPlace,
				WorldLocation = tileMap.CellToWorld(localPlace),
				TileBase = tileMap.GetTile(localPlace),
				TilemapMember = tileMap,
				Name = localPlace.x + "," + localPlace.y,
			    IntValue = 0
			};
			
            tileList.Add(tile);
		}

        return tileList;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)){
            var currentPostion = _player.transform.position;
            currentPostion.y = currentPostion.y + .01f;
            _player.transform.SetPositionAndRotation(currentPostion, new Quaternion(0,0,0,0));
            UpdateCurrentTile();
        }
        if (Input.GetKey(KeyCode.A)){
             var currentPostion = _player.transform.position;
            currentPostion.x = currentPostion.x - .01f;
            _player.transform.SetPositionAndRotation(currentPostion, new Quaternion(0,0,0,0));
            UpdateCurrentTile();
        }
        if (Input.GetKey(KeyCode.S)){
             var currentPostion = _player.transform.position;
            currentPostion.y = currentPostion.y - .01f;
            _player.transform.SetPositionAndRotation(currentPostion, new Quaternion(0,0,0,0));
            UpdateCurrentTile();
        }
        if (Input.GetKey(KeyCode.D)){
             var currentPostion = _player.transform.position;
            currentPostion.x = currentPostion.x + .01f;
            _player.transform.SetPositionAndRotation(currentPostion, new Quaternion(0,0,0,0));
            UpdateCurrentTile();

        }
        if (Input.GetKeyUp(KeyCode.Space)){
            Debug.Log("space");
            Instantiate(this._PrefabTilledTile, _currentTile.LocalPlace, Quaternion.identity);
            _currentTile.IsTilled = true;
        }
        if (Input.GetKeyUp(KeyCode.P)){
            Debug.Log("plant");
            if (this._currentTile.IsTilled){
                Instantiate(this._PrefabPot, _currentTile.LocalPlace, Quaternion.identity);
            }
        }
    }

    public void UpdateCurrentTile(){
        var v3Int = new Vector3Int((int)_player.transform.position.x, (int)_player.transform.position.y);
        this._currentTile = _worldTiles.FirstOrDefault(x => x.LocalPlace == v3Int);
        this._currentTileIndicator.transform.position = v3Int;
    }


    [System.Serializable]
    public class WorldTile {
        public Vector3Int LocalPlace;
        public Vector3 WorldLocation;
        public TileBase TileBase;
        public Tilemap TilemapMember;
        public string Name;

        public bool IsTilled;
        public bool IsPlant;
        public int IntValue;
        public bool ContainsPoop;

    }
}
