import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {SetHistoryStatusRequest} from "../models/set-history-status.request";

@Injectable({
  providedIn: 'root'
})
export class EducationAdminService {
  constructor(private readonly httpClient: HttpClient) {
  }

  getStatus(userId: number, exerciseId: number) {
    const params = new HttpParams().set("userId", userId).set("exerciseId", exerciseId);
    return this.httpClient.get('api/admin/education', {params: params});
  }

  setStatus(userId: number, exerciseId: number, status: boolean) {
    const request: SetHistoryStatusRequest = {userId: userId, exerciseId: exerciseId, status: status,};
    return this.httpClient.put('api/admin/education', request);
  }
}
