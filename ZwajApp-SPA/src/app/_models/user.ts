import { Photo } from "./photo";

export interface User {
  id: number;
  username: string;
  knownAs: string;
  age: number;
  gender: string;
  created: Date;
  lastActive: Date;
  photoURL: string;
  city: string;
  country: string;
  interests?: string; // Elvis Operator (Safe Navigation)
  introduction?: string;
  lookingFor?: string;
  photos?: Photo[];
}
