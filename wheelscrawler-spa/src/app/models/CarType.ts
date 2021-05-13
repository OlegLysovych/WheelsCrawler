import { Car } from './Car';

export interface CarType {
  Id: number;
  WheelsName: string;
  Cars?: Car[];
}
