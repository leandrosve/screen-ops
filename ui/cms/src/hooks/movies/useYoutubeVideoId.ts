import { useMemo } from "react";

export function useYoutubeVideoId(url?: string) {
  return useMemo(() => {
    if (!url) return null;

    try {
      const parsed = new URL(url);

      if (
        parsed.hostname === "youtu.be"
      ) {
        // Corto: https://youtu.be/VIDEO_ID
        return parsed.pathname.slice(1);
      }

      if (
        parsed.hostname.includes("youtube.com")
      ) {
        if (parsed.pathname.startsWith("/watch")) {
          return parsed.searchParams.get("v");
        }

        if (parsed.pathname.startsWith("/embed/")) {
          return parsed.pathname.split("/embed/")[1];
        }
      }

      return null;
    } catch {
      return null;
    }
  }, [url]);
}