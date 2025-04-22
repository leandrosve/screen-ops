import Role from "./Role";

interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  role: Role;
  joinedAt: string | Date;
}

export default User ;
