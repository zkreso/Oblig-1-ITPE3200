import { Symptom } from "./Symptom";

export class Disease {
  Id: number | undefined;
  Name: string | undefined;
  Symptoms: Symptom[] | undefined;
}
