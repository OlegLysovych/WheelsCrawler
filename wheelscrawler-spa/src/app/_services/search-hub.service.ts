import { Injectable } from '@angular/core';
import { HubConnection } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SearchHubService {
  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;

  constructor() { }
}
