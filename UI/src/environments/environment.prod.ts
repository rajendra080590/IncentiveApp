export const environment = {
  production: true,
  name: 'dev',
  clientId: 'fe03b1ef-5d8d-4985-a6ee-aec8c69c825b',

  authority: 'https://login.microsoftonline.com/8db90014-ca66-493d-b1b1-aa9ce0ba9aed/', 
  tenantId: '8db90014-ca66-493d-b1b1-aa9ce0ba9aed', 

  //redirectUrl: 'https://incentiveprodwebapp.azurewebsites.net/#/login', //PROD 
  redirectUrl: 'https://piecerateincentiveapp.fbmsales.com/#/login', //PROD Custom URL

  postLogoutRedirectUri: 'https://incentiveprodwebapp.azurewebsites.net/#/logout', //PROD

  scopeUri : "api://b50cae15-5f66-4c55-962e-dbd115ff8d23/incentive-api-access", //Prod

  uiClienId: '970ed1e2-f904-44a9-b654-2beeaa691b1c', // this is for Prod IncentiveWebApp
  
  mainUrl : 'https://incentiveprodwebapi.azurewebsites.net/'//PROD
};
