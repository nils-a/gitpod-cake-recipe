#tool dotnet:?package=GitVersion.Tool&version=5.6.8
#addin nuget:?package=Cake.Docker&version=1.0.0

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var imageName = Argument("imageName", "cake-recipe-gitpod");

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

var imageFullTag = "";
Task("Calculate-Image-Tag")
   .Does(() =>
{
   var version = GitVersion();
   imageFullTag = $"{imageName}:{version.SemVer}";
   Information($"calculated tag for image: {imageFullTag}");
});


Task("Build-Image")
   .IsDependentOn("Calculate-Image-Tag")
   .Does(() =>
{
   DockerBuild(new DockerImageBuildSettings 
   {
      Tag = new[] {imageFullTag},
   }, "src");
});

Task("Default")
.IsDependentOn("Build-Image");

RunTarget(target);