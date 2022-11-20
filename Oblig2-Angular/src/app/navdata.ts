export enum RouteStrings {
  Home = '',
  AddPage = 'create-disease',
  EditPage = 'edit-disease/:id',
  DiseaseList = 'disease-list',
  DiseaseDetails = 'disease-details/:id',
  NotFound = '404'
}

export interface NavLink {
  routeString: string;
  linkText: string;
  needsAuth: boolean;
}

export const navData: NavLink[] = [
  { routeString: RouteStrings.Home, linkText: "Home", needsAuth: false },
  { routeString: RouteStrings.AddPage, linkText: "Save New Disease", needsAuth: false },
  { routeString: RouteStrings.DiseaseList, linkText: "Disease List", needsAuth: true }
]
