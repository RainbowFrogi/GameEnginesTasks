using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystemPrefab;
    [SerializeField] Button generateButton;
    [SerializeField] Sprite[] shapeSprites; // Array of basic shape sprites
    
    private ParticleSystem particleEffect;

    void OnGenerateButtonClicked()
    {
        GenerateParticleEffect();
    }

    public void GenerateParticleEffect()
    {
        // Destroy previous effect if it exists
        if (particleEffect != null)
        {
            Destroy(particleEffect.gameObject);
        }

        particleEffect = Instantiate(particleSystemPrefab, gameObject.transform);
        
        var main = particleEffect.main;
        var shape = particleEffect.shape;
        var sizeOverLifetime = particleEffect.sizeOverLifetime;
        var colorOverLifetime = particleEffect.colorOverLifetime;
        var emission = particleEffect.emission;
        var rotationOverLifetime = particleEffect.rotationOverLifetime;
        var particleSystemRenderer = particleEffect.GetComponent<ParticleSystemRenderer>();
        // 1. Randomize start lifetime
        main.startLifetime = Random.Range(1f, 5f);
        
        // 2. Randomize start speed
        main.startSpeed = Random.Range(1f, 10f);
        
        // 3. Randomize start size
        main.startSize = Random.Range(0.1f, 2f);
        
        // 4. Randomize start color
        main.startColor = new ParticleSystem.MinMaxGradient(
            new Color(Random.value, Random.value, Random.value),
            new Color(Random.value, Random.value, Random.value)
        );
        
        // 5. Randomize start rotation
        main.startRotation = Random.Range(0, Mathf.PI * 2);
        
        // 6. Randomize gravity modifier
        main.gravityModifier = Random.Range(-1f, 1f);

        // 7. Randomize simulation space
        shape.radius = Random.Range(0.1f, 3f);

        // 8. Randomize amount of particles
        emission.rateOverTime = Random.Range(1, 4);

        // 9. Randomize rotation over lifetime
        rotationOverLifetime.enabled = Random.value > 0.5f;
        if (rotationOverLifetime.enabled)
        {
            rotationOverLifetime.z = new ParticleSystem.MinMaxCurve(Random.Range(-30f, 30f));
        }
        
        Sprite randomShape = shapeSprites[Random.Range(0, shapeSprites.Length)];

        Material material = particleSystemRenderer.material;

        material.mainTexture = randomShape.texture;
    }
}
