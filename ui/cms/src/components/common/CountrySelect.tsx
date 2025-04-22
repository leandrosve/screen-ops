import countries from "@/static/countries";
import { Select } from "chakra-react-select";

interface CountryOption {
  label: string;
  value: string;
}

interface CountrySelectProps {
  value?: CountryOption | null;
  onChange: (option: CountryOption | null) => void;
}

export default function CountrySelect({ value, onChange }: CountrySelectProps) {
  return (
    <Select
      options={countries}
      value={value}
      onChange={onChange}
      placeholder="Selecciona un país"
      isClearable
    />
  );
}
