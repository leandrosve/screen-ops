import User  from '../user/User';

export interface SessionData {
  token: string;
  expiresAt: Date,
  createdAt: Date,
  user: User;
}