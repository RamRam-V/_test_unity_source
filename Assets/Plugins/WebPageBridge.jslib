var WebPageBridge =
{
    AvatarLoadingCompleted: function()
    {
	ReactUnityWebGL.avatarLoadingCompleted();
    },

    AvatarLoadFail: function()
    {
	ReactUnityWebGL.avatarLoadFail();
    }
};

mergeInto(LibraryManager.library, WebPageBridge);