import User from "../user/User";

interface AuthResponseDto {
  user: User;
  token: string;
  expiresAt: Date;
  createdAt: Date;
}

export default AuthResponseDto;
