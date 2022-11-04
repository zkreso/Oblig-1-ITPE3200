export interface Symptom {
  id: number;
  name: string;
}

export interface Disease {
  id?: number;
  name: string;
  description: string;
  symptoms: string[];
}

export interface DiseaseEntity {
  name: string;
  description: string;
  diseaseSymptoms?: DiseaseSymptom[];
}

export interface DiseaseSymptom {
  symptomId?: number;
  diseaseId?: number;
}

export interface PageOptions {
  orderByOptions?: string;
  selectedSymptoms: Symptom[];
  searchString?: string;
  pageNum?: number;
  pageSize?: number;
  numEntries?: number;
  numPages?: number;
}

export interface SymptomsTable {
  pageData: PageOptions,
  symptomList: Symptom[]
}

export interface Alert {
  status: boolean;
  successText: string;
  failText: string;
}

export interface User {
  username: string;
  password: string;
}
