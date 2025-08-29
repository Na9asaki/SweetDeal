using SweetDeal.Source.Gadgets.Inventory.SpriteConfigurable;
using UnityEngine;

namespace SweetDeal.Source.Gadgets
{
    public class Oil : Gadget
    {
        private JarOil _oilProjectile;
        private Transform _spawnPoint;
        private float _throwHeight;
        private float _throwForce;

        private Transform _player;

        public Oil(Transform spawnPoint, Transform player, int useNumbers, GadgetDefinition definition) : base(useNumbers, definition)
        {
            _oilProjectile = definition.Projectile as JarOil;
            _spawnPoint = spawnPoint;
            _throwHeight = definition.ThrowHeight;
            _throwForce = definition.ThrowForce;
            _player = player;
        }
        
        public override void Use()
        {
            UseNumbers -= 1;
            var jarOil = GameObject.Instantiate(_oilProjectile, _spawnPoint);
            jarOil.transform.position = _spawnPoint.position;
            jarOil.transform.rotation = _spawnPoint.rotation;
            var direction = -_player.forward;
            direction.y = _throwHeight;
            jarOil.Launch(direction, _throwForce);
        }
    }
}