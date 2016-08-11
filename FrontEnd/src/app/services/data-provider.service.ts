import { Injectable } from '@angular/core'
import { Observable } from 'rxjs/Observable'
import { Http, Response } from '@angular/http'
import { PagingData } from '../model/models'

@Injectable()
export class DataProviderService {

    /**
     * get data provider
     * @param http
     */
    constructor(private http: Http) { }

    /**
     * Get Metadata of the entity
     * @param entity
     */
    getMetaData(entity: string) {
        return this.http.get("api/" + entity + "/metadata")
    }


    /**
     * query data 
     */
    queryData<T>(entity: string, 
                filters: any = undefined,
                start: number = 0,
                limit: number = 10,
                sortBy: string = undefined,
                isAscending: boolean = true,
                includeMetaData: boolean = false) {
        var query = "api/" + entity + "?"

        if (filters !== undefined) {
            query += "filters=" + encodeURIComponent(JSON.stringify(filters)) + "&"
        }

        query += "start=" + start + "&limit=" + limit + "&"
        
        if (sortBy !== undefined) {
            query += "orderBy=" + encodeURIComponent(sortBy) + "&isAscending=" + isAscending + "&"
        }

        query += "includeMetaData=" + includeMetaData + "&"

        return this.http.get(query).map(item => item.json() as PagingData<T>)
    }

    /**
     * Query items
     */
    queryItem(entity: string,
                id: string) {
        return this.http.get("api/" + entity + "/" + id)
    }

}
