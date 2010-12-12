/***************************************
* Lighting functions
***************************************/

/*********************************
* CalculateAmbient - 
* inputs - 
*	vKa material's reflective color
*	lightColor - the ambient color of the lightsource
* output - ambient color
*********************************/
float3 CalculateAmbient(float3 vKa, float3 lightColor)
{
	float3 vAmbient = vKa * lightColor;
	
	return vAmbient;
}

/*********************************
* CalculateDiffuse - 
* inputs - 
*	material color
*   The color of the direct light
*	the local normal
*   the vector of the direct light
* output - difuse color
*********************************/
float3 CalculateDiffuse(float3 baseColor, float3 lightColor, float3 normal, float3 lightVector)
{
	float3 vDiffuse = baseColor * lightColor * saturate(dot(normal, lightVector));
	
	return vDiffuse;
}

float specpower = 32.0f;
/*********************************
* CalculateSpecular - 
* inputs - 
*	viewVector
*	the direct light vector
*   the normal
* output - specular highlight
*********************************/
float CalculateSpecular(float3 viewVector, float3 lightVector, float3 normal)
{
	float3 vReflect = reflect(lightVector, normal);
	
	float fSpecular = saturate(dot(vReflect, viewVector));
	fSpecular = pow(fSpecular, specpower);
	
	return fSpecular;
}

/*********************************
* LightingCombine - 
* inputs - 
*	ambient component
*	diffuse component
*   specualr component
* output - phong color color
*********************************/
float3 LightingCombine(float3 vAmbient, float3 vDiffuse, float fSpecular)
{
	float3 vCombined = vAmbient + vDiffuse + fSpecular.xxx;
	
	return vCombined;
}
