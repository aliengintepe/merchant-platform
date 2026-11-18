import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'merchantPlatform',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44380/',
    redirectUri: baseUrl,
    clientId: 'merchantPlatform_App',
    responseType: 'code',
    scope: 'offline_access merchantPlatform',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44380',
      rootNamespace: 'merchantPlatform',
    },
  },
} as Environment;
