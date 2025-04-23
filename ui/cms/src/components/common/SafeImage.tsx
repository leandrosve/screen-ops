import { Image, ImageProps } from "@chakra-ui/react";
import { useState } from "react";

interface Props extends ImageProps {
    placeholder?: string;
}

const SafeImage = ({placeholder = "/placeholder.jpg", ...props }: Props) => {
  const [error, setError] = useState(false);

  return (
    <Image
      {...props}
      src={error ? placeholder : (props.src ?? placeholder)}
      onError={() => setError(true)}
    />
  );
};

export default SafeImage;
