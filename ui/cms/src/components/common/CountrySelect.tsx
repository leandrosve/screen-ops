import countries from "@/static/countries";
import { Select } from "chakra-react-select";
import { useMemo } from "react";

interface CountryOption {
  label: string;
  value: string;
}

interface CountrySelectProps {
  value?: string | null; // countryCode
  onChange: (option: CountryOption | null) => void;
}

export default function CountrySelect({ value, onChange }: CountrySelectProps) {
  const valueOption = useMemo(() => {
    return countries.find((v) => v.value == value) ?? null;
  }, [value]);
  return (
    <Select
      options={countries}
      value={valueOption}
      onChange={onChange}
      placeholder="Selecciona un país"
      isClearable
    />
  );
}
