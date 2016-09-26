//Copyright (C) 2016 Jason Mendel and Binary System Games - All Rights Reserved
//Contact Admin@BinarySystemGames.com for help or information

#pragma strict
#pragma downcast

var myTarget : Transform;

var lightningMaterial : Material;
var glowMaterial : Material;

var redrawSpeed : float;
private var redrawTimer : float;

var ThreeD : boolean;
var Glow : boolean;

var lightningStepSize : float;
var lightningStepRadius: float;
var lightningBranches : int;
var branchPotential : float;

var lightningSizeStartMain : float;
var lightningSizeEndMain : float;
var lightningSizeStartBranch : float;
var lightningSizeEndBranch : float;

var FullBranchIsMain : boolean;
var BranchesSpreading : boolean;
var SpreadFromBranches : boolean;
var TreedBranches : boolean;
var BranchSpreadRadius : float;
var BranchShortening : float;


private var renderArray : Array = new Array();
private var glowArray : Array = new Array();
private var branchPoints : Array = new Array();
private var totalBranchSegments : int;
private var lastKnownTargetPos : Vector2;
private var lastKnownTargetPosThree : Vector3;


var GlowRange : float;

function Start ()
{
	if(BranchShortening < 0)
	{
	 	BranchShortening = 0;
	}
	renderArray.push(gameObject);
	var tempRenderer : GameObject = new GameObject(); 
	tempRenderer.name = "Lightning Effect Temp Renderer";
	GetComponent(LineRenderer).material = lightningMaterial;
	for(var i = 0; i < lightningBranches; i++)
	{
		var childRenderer : GameObject = Instantiate(tempRenderer);
     	childRenderer.transform.parent = transform;
     	childRenderer.name = "lightning branch";
     	childRenderer.AddComponent(LineRenderer);
     	childRenderer.GetComponent(LineRenderer).material = lightningMaterial;
		renderArray.push(childRenderer);
	}
	
	if(Glow)
	{
		for(i = 0; i < renderArray.Count; i++)
		{
			var currentRenderer : GameObject = renderArray[i] as GameObject;
			var childRendererGlow : GameObject = Instantiate(tempRenderer);
	     	childRendererGlow.transform.parent = currentRenderer.transform;
	     	childRendererGlow.name = "lightning branch Glow";
	     	childRendererGlow.AddComponent(LineRenderer);
	     	childRendererGlow.GetComponent(LineRenderer).material = glowMaterial;
			glowArray.push(childRendererGlow);
		}
	}
	
	lastKnownTargetPos = myTarget.position;
	lastKnownTargetPosThree = myTarget.position;
	Destroy(tempRenderer);
}

function AddTarget(newTarget : Transform)
{
	myTarget = newTarget;
}

function Update () 
{
	if(myTarget)
	{
		lastKnownTargetPos = myTarget.position;
		lastKnownTargetPosThree = myTarget.position;
	}
	redrawTimer -= Time.deltaTime;
	if(redrawTimer <= 0)
	{
		if(BranchShortening < 0)
		{
		 	BranchShortening = 0;
		}
		CreateBranchedLightning();
		UpdateGlow();
		redrawTimer = redrawSpeed;
	}
}

function UpdateGlow()
{
	if(!Glow && glowArray.length > 0)
	{
		for(var i = 0; i < glowArray.length; i++)
		{
			var currentRendererGlow : GameObject = glowArray[i] as GameObject;
			currentRendererGlow.GetComponent(LineRenderer).enabled = false;
		}
	}
}


private function CreateBranchedLightning()
{
	var MainBranchNumber : int = 0;
	if(!FullBranchIsMain)
	{
	 	MainBranchNumber = Random.Range(0, renderArray.length);
	}
		

	if(ThreeD == true)
	{
		var startingPointThree : Vector3 = transform.position;
		var	numPointsThree = Vector3.Distance(startingPointThree, lastKnownTargetPosThree)/lightningStepSize;
		totalBranchSegments = renderArray.length * numPointsThree;
		branchPoints.clear();
		var newPositionThree: Vector3 = lastKnownTargetPosThree;
		for(var i = 0; i < renderArray.length; i++)
		{	
			var currentRendererThree = renderArray[i] as GameObject;
			var currentRendererGlow : GameObject;
			if(Glow)
			{
				currentRendererGlow = glowArray[i] as GameObject;
			}
				
			if(BranchesSpreading && i != MainBranchNumber )
			{
				if(SpreadFromBranches)
				{
					newPositionThree = (newPositionThree + Random.insideUnitSphere * BranchSpreadRadius  );
				}
				else
				{
					newPositionThree = (lastKnownTargetPosThree + Random.insideUnitSphere * BranchSpreadRadius  );
				}
				
				if(TreedBranches)
				{
					numPointsThree = Vector3.Distance(startingPointThree, newPositionThree)/lightningStepSize * BranchShortening / i;
				}
				else
				{
					numPointsThree = Vector3.Distance(startingPointThree, newPositionThree)/lightningStepSize * BranchShortening;
				}
				
				if(Glow)
				{
					CreateLightning(currentRendererThree.GetComponent(LineRenderer),currentRendererGlow.GetComponent(LineRenderer), startingPointThree, newPositionThree ,numPointsThree);
				}
				else
				{
					CreateLightning(currentRendererThree.GetComponent(LineRenderer), startingPointThree, newPositionThree ,numPointsThree);
				}
			}
			else
			{
				currentRendererThree.GetComponent(LineRenderer).enabled = true;
				currentRendererThree.GetComponent(LineRenderer).SetWidth(lightningSizeStartMain, lightningSizeEndMain);
				
				if(Glow)
				{
					currentRendererGlow.GetComponent(LineRenderer).enabled = true;
					currentRendererGlow.GetComponent(LineRenderer).SetWidth(lightningSizeStartMain*GlowRange, lightningSizeEndMain*GlowRange);
				}
				
				if(!FullBranchIsMain && i > 0)
				{
					var currentRendererTemp = renderArray[i-1] as GameObject;
					currentRendererTemp.GetComponent(LineRenderer).SetWidth(lightningSizeStartBranch, lightningSizeStartMain);
				}
				
				if(Glow)
				{
					CreateLightning(currentRendererThree.GetComponent(LineRenderer),currentRendererGlow.GetComponent(LineRenderer), startingPointThree, lastKnownTargetPosThree ,numPointsThree);
				}
				else
				{
					CreateLightning(currentRendererThree.GetComponent(LineRenderer), startingPointThree, lastKnownTargetPosThree ,numPointsThree);
				}
				
			}
			if(branchPoints.length)
			{
				startingPointThree = branchPoints.pop();
				numPointsThree = Vector3.Distance(startingPointThree, lastKnownTargetPosThree)/lightningStepSize;
				currentRendererThree.GetComponent(LineRenderer).enabled = true;
				
				if(Glow)
				{
					currentRendererGlow.GetComponent(LineRenderer).enabled = true;
				}
				
				if(i != MainBranchNumber)
				{
					currentRendererThree.GetComponent(LineRenderer).SetWidth(lightningSizeStartBranch, lightningSizeEndBranch);
					if(Glow)
					{
						currentRendererGlow.GetComponent(LineRenderer).SetWidth(lightningSizeStartBranch*GlowRange, lightningSizeEndBranch*GlowRange);
					}
				}
				else
				{
					currentRendererThree.GetComponent(LineRenderer).SetWidth(lightningSizeStartMain, lightningSizeEndMain);
					if(Glow)
					{
						currentRendererGlow.GetComponent(LineRenderer).SetWidth(lightningSizeStartMain*GlowRange, lightningSizeEndMain*GlowRange);
					}
				}
				
				if(numPointsThree < 2)
				{
					numPointsThree = 2;
				}
			}
			else
			{
				currentRendererThree.GetComponent(LineRenderer).enabled = false;
			}		
		}
	}
	else
	{
		var startingPoint : Vector2 = transform.position;
		var	numPoints = Vector2.Distance(startingPoint, lastKnownTargetPos)/lightningStepSize;
		totalBranchSegments = renderArray.length * numPoints;
		branchPoints.clear();
		var newPosition: Vector2 = lastKnownTargetPos;
		for(var z = 0; z < renderArray.length; z++)
		{	
			var currentRenderer = renderArray[z] as GameObject;
			var currentGlow : GameObject;
			if(Glow)
			{
				currentGlow = glowArray[z] as GameObject;
			}
			
			if(BranchesSpreading && z != MainBranchNumber  )
			{
				if(SpreadFromBranches)
				{
					newPosition = (newPosition + Random.insideUnitSphere * BranchSpreadRadius  );
				}
				else
				{
					newPosition = (lastKnownTargetPos + Random.insideUnitSphere * BranchSpreadRadius / z);
				}
				
				if(TreedBranches)
				{
					numPoints = Vector2.Distance(startingPoint, newPosition)/lightningStepSize * BranchShortening / z;
				}
				else
				{
					numPoints = Vector3.Distance(startingPoint, newPosition)/lightningStepSize * BranchShortening;
				}
				
				if(Glow)
				{
					CreateLightning(currentRenderer.GetComponent(LineRenderer),currentGlow.GetComponent(LineRenderer), startingPoint, newPosition ,numPoints);
				}
				else
				{
					CreateLightning(currentRenderer	.GetComponent(LineRenderer), startingPoint, newPosition ,numPoints);
				}
				
			}
			else
			{
				currentRenderer.GetComponent(LineRenderer).enabled = true;
				currentRenderer.GetComponent(LineRenderer).SetWidth(lightningSizeStartMain, lightningSizeEndMain);
				if(Glow)
				{
					currentGlow.GetComponent(LineRenderer).enabled = true;
					currentGlow.GetComponent(LineRenderer).SetWidth(lightningSizeStartMain*GlowRange, lightningSizeEndMain*GlowRange);
				}
				
				if(!FullBranchIsMain && z > 0)
				{
					var currentRendererTempNew = renderArray[z-1] as GameObject;
					currentRendererTempNew.GetComponent(LineRenderer).SetWidth(lightningSizeStartBranch, lightningSizeStartMain);
				}
				
				if(Glow)
				{
					CreateLightning(currentRenderer.GetComponent(LineRenderer),currentGlow.GetComponent(LineRenderer), startingPoint, lastKnownTargetPos ,numPoints);
				}
				else
				{
					CreateLightning(currentRenderer.GetComponent(LineRenderer), startingPoint, lastKnownTargetPos ,numPoints);
				}
			}
			if(branchPoints.length)
			{
				currentRenderer.GetComponent(LineRenderer).enabled = true;
				
				if(Glow)
				{
					currentGlow.GetComponent(LineRenderer).enabled = true;
				}
				
				if(z != MainBranchNumber)
				{
					currentRenderer.GetComponent(LineRenderer).SetWidth(lightningSizeStartBranch, lightningSizeEndBranch);
					if(Glow)
					{
						currentGlow.GetComponent(LineRenderer).SetWidth(lightningSizeStartBranch*GlowRange, lightningSizeEndBranch*GlowRange);
					}
				}
				else
				{
					currentRenderer.GetComponent(LineRenderer).SetWidth(lightningSizeStartMain, lightningSizeEndMain);
					if(Glow)
					{
						currentGlow.GetComponent(LineRenderer).SetWidth(lightningSizeStartMain*GlowRange, lightningSizeEndMain*GlowRange);
					}
				}
				startingPoint = branchPoints.pop();
				numPoints = Vector2.Distance(startingPoint, lastKnownTargetPos)/lightningStepSize;
				if(numPoints < 2)
				{
					numPoints = 2;
				}
			}
			else
			{
				currentRenderer.GetComponent(LineRenderer).enabled = false;
			}		
		}
	}
}


private function CreateLightning(lineRenderer : LineRenderer, startingPosition : Vector2, endPosition : Vector2, numPoints : int)
{
	

	if(numPoints < 3)
	{
		numPoints = 3;
	}
	
	lineRenderer.SetVertexCount(numPoints);
	
	lineRenderer.SetPosition(0, startingPosition);
	
	if(lightningBranches <= 0)
	{
		lightningBranches = 1;
	}
	
	var currentPosition : Vector2 = startingPosition;
	var towardsTarget : Vector2;
	for(var i = 1; i < numPoints; i++)
	{
		towardsTarget = endPosition - currentPosition;
		towardsTarget.Normalize();
		currentPosition += towardsTarget * lightningStepSize;
		currentPosition += Vector2(Random.Range(-lightningStepRadius, lightningStepRadius),Random.Range(-lightningStepRadius, lightningStepRadius));

		lineRenderer.SetPosition(i, currentPosition);
		if(branchPoints.length <= ((i/lightningBranches)*2))
		{
			HandleBranchPotential(currentPosition);
		}
	}
}

private function CreateLightning(lineRenderer : LineRenderer, glowRenderer : LineRenderer, startingPosition : Vector2, endPosition : Vector2, numPoints : int)
{
	

	if(numPoints < 3)
	{
		numPoints = 3;
	}
	
	lineRenderer.SetVertexCount(numPoints);
	glowRenderer.SetVertexCount(numPoints);
	lineRenderer.SetPosition(0, startingPosition);
	glowRenderer.SetPosition(0, startingPosition);
	if(lightningBranches <= 0)
	{
		lightningBranches = 1;
	}
	
	var currentPosition : Vector2 = startingPosition;
	var towardsTarget : Vector2;
	for(var i = 1; i < numPoints; i++)
	{
		towardsTarget = endPosition - currentPosition;
		towardsTarget.Normalize();
		currentPosition += towardsTarget * lightningStepSize;
		currentPosition += Vector2(Random.Range(-lightningStepRadius, lightningStepRadius),Random.Range(-lightningStepRadius, lightningStepRadius));

		lineRenderer.SetPosition(i, currentPosition);
		glowRenderer.SetPosition(i, currentPosition);
		if(branchPoints.length <= ((i/lightningBranches)*2))
		{
			HandleBranchPotential(currentPosition);
		}
	}
}

private function CreateLightning(lineRenderer : LineRenderer, startingPosition : Vector3, endPosition : Vector3, numPoints : int)
{
	

	if(numPoints < 3)
	{
		numPoints = 3;
	}
	
	lineRenderer.SetVertexCount(numPoints);
	
	lineRenderer.SetPosition(0, startingPosition);
	
	if(lightningBranches <= 0)
	{
		lightningBranches = 1;
	}
	
	var currentPosition : Vector3 = startingPosition;
	var towardsTarget : Vector3;
	for(var i = 1; i < numPoints; i++)
	{
		towardsTarget = endPosition - currentPosition;
		towardsTarget.Normalize();
		
		currentPosition += towardsTarget * lightningStepSize;
		currentPosition += Vector3(Random.Range(-lightningStepRadius, lightningStepRadius),Random.Range(-lightningStepRadius, lightningStepRadius), 0);

		lineRenderer.SetPosition(i, currentPosition);
		if(branchPoints.length <= ((i/lightningBranches)*2))
		{
			HandleBranchPotential(currentPosition);
		}
	}
}

private function CreateLightning(lineRenderer : LineRenderer, glowRenderer : LineRenderer, startingPosition : Vector3, endPosition : Vector3, numPoints : int)
{
	
	if(numPoints < 3)
	{
		numPoints = 3;
	}
	
	lineRenderer.SetVertexCount(numPoints);
	glowRenderer.SetVertexCount(numPoints);
	lineRenderer.SetPosition(0, startingPosition);
	glowRenderer.SetPosition(0, startingPosition);
	if(lightningBranches <= 0)
	{
		lightningBranches = 1;
	}
	
	var currentPosition : Vector3 = startingPosition;
	var towardsTarget : Vector3;
	for(var i = 1; i < numPoints; i++)
	{
		towardsTarget = endPosition - currentPosition;
		towardsTarget.Normalize();
		
		currentPosition += towardsTarget * lightningStepSize;
		currentPosition += Vector3(Random.Range(-lightningStepRadius, lightningStepRadius),Random.Range(-lightningStepRadius, lightningStepRadius), 0);

		lineRenderer.SetPosition(i, currentPosition);
		glowRenderer.SetPosition(i, currentPosition);
		if(branchPoints.length <= ((i/lightningBranches)*2))
		{
			HandleBranchPotential(currentPosition);
		}
	}
}

private function HandleBranchPotential(positionToBranch : Vector2)
{
	totalBranchSegments--;
	if(lightningBranches > branchPoints.length)
	{		
		if((Random.value <= branchPotential) || totalBranchSegments < lightningBranches)
		{
			branchPoints.push(positionToBranch);
		}		
	}
}

private function HandleBranchPotential(positionToBranch : Vector3)
{
	totalBranchSegments--;
	if(lightningBranches > branchPoints.length)
	{		
		if((Random.value <= branchPotential) || totalBranchSegments < lightningBranches)
		{
			branchPoints.push(positionToBranch);
		}		
	}
}
