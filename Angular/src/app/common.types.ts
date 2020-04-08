
export type person = {
    name: string;
    gender: string;
    age: number;
    pets?: pet[];
  }

  export type pet = {
    name: string;
    type: string;
  }