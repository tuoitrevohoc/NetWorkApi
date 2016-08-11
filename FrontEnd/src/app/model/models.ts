


/**
 * Column Data class
 */
export interface ColumnData {

    /// name of the column
    name: string

    /// display name of the columns
    displayName: string

    /// the description of the object
    description: string

    /// validations
    validations: any[]

}


/**
 * the application model
 **/
export interface AppModel {

    /// the id of user
    id: string
}

/**
 * user class
 */
export interface User {

    /// email of user
    email: string

    /// fullname of the user
    fullName: string

    /// avatar
    avatar: string

}

