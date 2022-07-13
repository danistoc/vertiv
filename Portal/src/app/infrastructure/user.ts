export interface User {
    id: number;
    username: string;
    password: string;
    passwordExpirationDate: string;
}

export interface UserViewModel {
    id: number;
    username: string;
    password: string;
    passwordExpirationDate: string;
    secondsRemaining: number;
    isValid: boolean;
}