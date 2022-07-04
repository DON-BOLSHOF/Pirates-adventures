using UnityEngine;

namespace Assets.scripts.Model.Weapons
{
    class ProjectTile : BaseProjectTile
    {
        protected override void Start()
        {
            base.Start();
            Rigidbody.AddForce(new Vector2(Direction * _speed, 0), ForceMode2D.Impulse);
        }
    }
}
