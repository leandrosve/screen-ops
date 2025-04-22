interface Props {
  url: string;
  width?: string;
  height?: string;
}
const YouTubePlayer = ({ url, width = "560", height = "315" }: Props) => {
  return (
    <iframe
      width={width}
      height={height}
      src={url}
      frameBorder="0"
      allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
      referrerPolicy="strict-origin-when-cross-origin"
      allowFullScreen
    ></iframe>
  );
};

export default YouTubePlayer;
