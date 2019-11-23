import { Injectable } from '@angular/core';
import { ResourceType } from './resource.service';
import { Article } from 'src/app/models/Article';

@Injectable({
    providedIn: 'root'
})
export class ResourceSearchService {
    constructor() { }

    public filterResource(resourceType: ResourceType, resourceList: any[], question: string) {
        if (question === '' || question === null || question === undefined) {
            return resourceList;
        }

        switch (resourceType) {
            case ResourceType.Article:
                return this.filterArticles(resourceList, question);
            default:
                break;
        }
    }

    private filterArticles(resourceList: Article[], question: string) {
        let returnList = new Array<any>();

        let tmpList: any[];
        tmpList = resourceList.filter(x => x.title.includes(question));

        if (tmpList.length > 0) {
            returnList = returnList.concat(tmpList);
        }
        tmpList = resourceList.filter(x => x.author.includes(question));
        if (tmpList.length > 0) {
            returnList = returnList.concat(tmpList);
        }
        tmpList = resourceList.filter(x => x.text.includes(question));
        if (tmpList.length > 0) {
            returnList = returnList.concat(tmpList);
        }

        // TODO: Remove duplicates.

        return returnList;
    }
}
