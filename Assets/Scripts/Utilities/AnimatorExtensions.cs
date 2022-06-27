using System.Text;
using UnityEngine;

namespace Utilities
{
    public static class AnimatorExtensions
    {
        // Sometimes not working as expected.
        public static float GetCurrentClipLength(this Animator animator)
        {
            var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            return clipInfo[0].clip.length;
        }

        //index starts from zero.
        public static float GetClipLengthFromClipIndex(this Animator animator, int clipIndex)
        {
            var clips = animator.runtimeAnimatorController.animationClips;
            return clips[clipIndex].length;
        }

        //index starts from zero.
        public static string GetClipNameFromClipIndex(this Animator animator, int clipIndex)
        {
            var clips = animator.runtimeAnimatorController.animationClips;
            return clips[clipIndex].name;
        }
        
        public static string GetCurrentClipName(this Animator animator)
        {
            var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            return clipInfo[0].clip.name;
        }

        public static void DebugCurrentClipInfo(this Animator animator)
        {
            var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var info in clipInfo)
            {
                stringBuilder.AppendLine($"Clip Name: {info.clip.name}, Clip Length: {info.clip.length}");
            }
            Debug.Log(stringBuilder.ToString());
        }
        
        public static void DebugAllClipInfo(this Animator animator)
        {
            var clips = animator.runtimeAnimatorController.animationClips;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var clip in clips)
            {
                stringBuilder.AppendLine($"Clip name: {clip.name}, Clip Length: {clip.length}");
            }
            Debug.Log(stringBuilder.ToString());
        }


    }
}
