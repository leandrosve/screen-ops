import { useMemo } from "react";

interface YoutubeEmbedResult {
  embedUrl: string | null;
  error: string | null;
}

export function useYoutubeEmbedUrl(url?: string | null): YoutubeEmbedResult {
  return useMemo(() => {

    if (!url) {
      return { embedUrl: null, error: null };
    }

    const regex = /(?:youtube\.com\/watch\?v=|youtu\.be\/)([\w-]+)/;
    const match = url.match(regex);
    const videoId = match?.[1];

    if (!videoId) {
      return { embedUrl: null, error: "La URL no corresponde a un video de YouTube." };
    }

    return { embedUrl: `https://www.youtube.com/embed/${videoId}`, error: null };
  }, [url]);
}
